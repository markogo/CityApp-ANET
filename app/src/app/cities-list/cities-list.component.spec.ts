import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { WrapperComponent } from '../wrapper/wrapper.component';

import { CitiesListComponent } from './cities-list.component';

describe('CitiesListComponent', () => {
  let component: CitiesListComponent;
  let fixture: ComponentFixture<CitiesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        MatPaginatorModule,
        BrowserAnimationsModule,
        MatInputModule,
      ],
      providers: [{ provide: MatDialog, useValue: {} }],
      declarations: [CitiesListComponent, WrapperComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CitiesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
