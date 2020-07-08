import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Book, BookList, AuthorList } from "./books.models";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class AuthorsService {

  constructor(private http: HttpClient) {

  }

  public getAll(): Observable<AuthorList> {
    return this.http.get<AuthorList>(this.getUrl() + '/api/author');
  }

  private getUrl(): string {
    return 'http://localhost:5000';
    //return window.location.host;
  }
}
