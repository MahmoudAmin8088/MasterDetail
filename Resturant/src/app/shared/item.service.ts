import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ItemService {
Uri='https://localhost:7299/api/Item';
  constructor(private Http:HttpClient) { }

  GetAll():Observable<any[]>{
    return this.Http.get<any[]>(this.Uri);
    }
}
