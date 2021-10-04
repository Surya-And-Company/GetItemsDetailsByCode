import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class FileuploadService {
  constructor(private http: HttpClient) {}

  upload(files: any[]): Observable<string[]> {
    let formData: FormData = new FormData();
    for(let f in files) {
      formData.append('files', files[f]);
    }
    return this.http.post<string[]>(
      `${environment.serviceUrlv1}File/Upload`,
      formData
    );
  }
}
