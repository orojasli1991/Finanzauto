import { Component, OnInit } from '@angular/core';
import { ProductService, Product } from '../services/product.service';
import { Router ,RouterModule} from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-products-list',
  imports: [CommonModule,RouterModule],
  templateUrl: './products-list.component.html',
  styleUrls: [] 
  
})
export class ProductsListComponent implements OnInit {

  products: Product[] = [];

  constructor(private productsService: ProductService, private router: Router) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.productsService.getAll().subscribe((data: Product[]) => {
      this.products = data;
    });
  }

  editProduct(id: number) {
     this.router.navigate(['/products/edit', id]);
  }

  deleteProduct(id: number): void {
    if (confirm('¿Eliminar producto?')) {
      this.productsService.delete(id).subscribe(() => this.loadProducts());
    }
  }
}

