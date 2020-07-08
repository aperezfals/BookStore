import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { Book, BookList } from "./books.models";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class BooksService {

  books$ = new Subject<Book[]>();
  bookUpserted$ = new Subject();

  constructor(private http: HttpClient) {

  }

  public getAll() {
    this.http.get<BookList>(this.getUrl() + '/api/book')
      .subscribe(books => {
        this.books$.next(books.books);
      });
  }

  public upsertBook(book: Book) {
    this.http.post<number>(this.getUrl() + '/api/book', book)
      .subscribe(id => {
        this.bookUpserted$.next();
      });
  }

  public deleteBook(bookId: number) {
    this.http.delete<number>(this.getUrl() + '/api/book/' + bookId)
      .subscribe(() => {
        this.bookUpserted$.next();
      });
  }

  private getUrl(): string {
    return 'http://localhost:5000';
    //return window.location.host;
  }

  
}
