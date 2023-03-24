import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthService } from '../services/auth.service';
import { CityService } from '../services/city.service';
import { City } from '../types/city';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css'],
})
export class EditDialogComponent {
  responseMessage = '';

  editForm = this.formBuilder.group({
    name: [this.city.name],
    photo: [this.city.photo],
  });

  constructor(
    private dialogRef: MatDialogRef<EditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public city: City,
    private formBuilder: FormBuilder,
    private cityService: CityService,
    private authService: AuthService
  ) {}

  close() {
    this.dialogRef.close();
  }

  resetResponseMessage() {
    this.responseMessage = '';
  }

  editCity() {
    const formValues = this.editForm.value;

    const editedCity: City = {
      id: this.city.id,
      name: formValues.name!,
      photo: formValues.photo!,
    };

    this.cityService.editCity(editedCity).subscribe({
      next: (response: HttpResponse<null>) => {
        if (response === null) {
          this.responseMessage = 'EDIT SUCCESSFUL';
          this.city.name = editedCity.name;
          this.city.photo = editedCity.photo;
        }
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.authService.logout();
        } else {
          this.responseMessage = 'EDIT UNSUCCESSFUL';
        }
      },
    });
  }
}
