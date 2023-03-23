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
  //TODO: SHOW ERRORMESSAGE IN TEMPLATE
  errorMessage = null;

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
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.error;
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

  searchCities() {
    const formValues = this.searchForm.value;
    if (formValues.name == '') {
      this.getCities(this.pageNumber, this.pageSize);
    } else {
      this.cityService
        .searchCities(this.pageNumber, this.pageSize, formValues.name!)
        .subscribe({
          next: (response: GetCitiesDTO) => {
            if (response) {
              this.cityData = response;
            }
          },
          error: (error: HttpErrorResponse) => {
            this.errorMessage = error.error;
          },
        });
    }
  }

  onPaginateChange(event: PageEvent) {
    let page = event.pageIndex;
    let size = event.pageSize;

    page = page + 1;

    this.getCities(page, size);
  }
}
