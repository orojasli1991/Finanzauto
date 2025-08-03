import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';

import { ProductFormComponent } from './product-form.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ActivatedRoute } from '@angular/router';

describe('ProductFormComponent', () => {
  let component: ProductFormComponent;
  let fixture: ComponentFixture<ProductFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductFormComponent, HttpClientTestingModule],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            params: of({ id: '123' }),  // Simula parámetros de ruta
            snapshot: {
              paramMap: {
                get: (key: string) => '123'  // Simula snapshot.paramMap.get()
              }
            }
          }
        }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('create', () => {
    expect(component).toBeTruthy();
  });

  it('render', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    const formElement = compiled.querySelector('form');
    expect(formElement).toBeTruthy();
  });

  it('form invalid', () => {
    component.form.controls['name'].setValue('');
    component.form.controls['price'].setValue(null);
    component.form.controls['stock'].setValue(null);
    component.form.controls['categoryId'].setValue(null);


    expect(component.form.valid).toBeFalsy();
  });

  it('form valid', () => {
    component.form.controls['name'].setValue('Producto ejemplo');
    component.form.controls['price'].setValue(10.5);
    component.form.controls['stock'].setValue(5);
    component.form.controls['categoryId'].setValue(5);
    

    expect(component.form.valid).toBeTruthy();
  });

});
