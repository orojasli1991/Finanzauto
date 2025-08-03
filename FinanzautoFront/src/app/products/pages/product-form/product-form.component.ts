import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup,ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService, Product } from '../services/product.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-product-form',
  standalone: true,
  templateUrl: './product-form.component.html',
  styleUrls: [],
  imports: [ReactiveFormsModule, CommonModule]
})
export class ProductFormComponent implements OnInit {


  productId?: number;
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      price: [0, [Validators.required, Validators.min(0.01)]],
      stock: [0, [Validators.required, Validators.min(1)]],
      categoryId : ['', Validators.required],
    });
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.productId = +idParam;
      this.loadProduct();
    }
  }

  loadProduct() {
    this.productService.getById(this.productId!).subscribe(product => {
      this.form.patchValue({
        name: product.name,
        description: product.description,
        price: product.price,
        stock: product.stock,
        categoryId: product.categoryId
      });
    });
  }

  saveProduct() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const productData: Product = {
      name: this.form.get('name')?.value ?? '',
      description: this.form.get('description')?.value ?? '',
      price: this.form.get('price')?.value ?? 0,
      stock: this.form.get('stock')?.value ?? 0,
      categoryId: this.form.get('categoryId')?.value ?? 0
    };


    if (this.productId) {
      this.productService.update(this.productId, productData).subscribe(() => {
        alert('Producto actualizado con éxito.');
        this.router.navigate(['/products']);
      });
    } else {
      this.productService.create(productData).subscribe(() => {
        alert('Producto creado con éxito.');
        this.router.navigate(['/products']);
      });
    }
  }
  cancel() {
  this.router.navigate(['/products']);
}
}
