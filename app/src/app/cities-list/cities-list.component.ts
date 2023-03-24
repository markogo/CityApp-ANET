import { Component } from '@angular/core';
import { CityService } from '../services/city.service';
import { HttpErrorResponse } from '@angular/common/http';
import { PageEvent } from '@angular/material/paginator';
import { GetCitiesDTO } from '../types/getCities';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { EditDialogComponent } from '../edit-dialog/edit-dialog.component';
import { City } from '../types/city';
import { AuthService } from '../services/auth.service';
import { Role } from '../types/user';

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css'],
})
export class CitiesListComponent {
  searchForm = this.formBuilder.group({
    name: [''],
  });

  pageSizeOptions = [5, 10, 25, 100];
  pageNumber = 1;
  pageSize = 10;
  cityData: GetCitiesDTO | null = null;
  canEditCity = false;

  constructor(
    private cityService: CityService,
    private formBuilder: FormBuilder,
    private editDialog: MatDialog,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.getCities(this.pageNumber, this.pageSize);
    this.canEditCity =
      this.authService.userProfile.value.role === Role.EDITOR ||
      this.authService.userProfile.value.role === Role.ADMIN;
  }

  getCities(pageNumber: number, pageSize: number) {
    this.cityService.getAllCities(pageNumber, pageSize).subscribe({
      next: (response: GetCitiesDTO) => {
        if (response) {
          this.cityData = response;
        }
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.authService.logout();
        }
      },
    });
  }

  editCity(city: City) {
    this.editDialog.open(EditDialogComponent, { data: city });
  }

  resetSearch() {
    this.searchForm.reset({ name: '' });
    this.getCities(this.pageNumber, this.pageSize);
  }

  onSearchClick() {
    this.searchCities(this.pageNumber, this.pageSize);
  }

  searchCities(pageNumber: number, pageSize: number) {
    const formValues = this.searchForm.value;
    if (formValues.name === null || formValues.name === '') {
      this.getCities(pageNumber, pageSize);
    } else {
      this.cityService
        .searchCities(pageNumber, pageSize, formValues.name!)
        .subscribe({
          next: (response: GetCitiesDTO) => {
            if (response) {
              this.cityData = response;
            }
          },
          error: (error: HttpErrorResponse) => {
            if (error.status === 401) {
              this.authService.logout();
            }
          },
        });
    }
  }

  onPaginateChange(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;

    if (this.searchForm.value.name != '') {
      this.searchCities(this.pageNumber, this.pageSize);
    } else {
      this.getCities(this.pageNumber, this.pageSize);
    }
  }
}
