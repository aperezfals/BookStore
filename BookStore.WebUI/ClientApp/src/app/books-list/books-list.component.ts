import { Component, OnInit } from '@angular/core';
import { Book } from '../books.models';
import { BooksService } from '../books.service';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html'
})
export class BooksListComponent implements OnInit {
  books: Book[];
  selectedBook: Book;
  isEditMode: boolean = false;

  constructor(private booksService: BooksService) {

  }

  ngOnInit(): void {
    this.booksService.books$
      .subscribe(books => {
        this.books = books;
      });

    this.booksService.bookUpserted$
      .subscribe(() => {
        this.isEditMode = false;
        this.booksService.getAll();
      });

    this.booksService.getAll();
  }

  selectBook(book: Book) {
    this.selectedBook = Object.assign({}, book);
    this.isEditMode = true;
  }

  addBook() {
    this.selectedBook = {
      id: null,
      authorId: 1,
      isbn: '',
      name: '',
      author: null
    };
    this.isEditMode = true;
  }

  deleteBook(book) {
    this.booksService.deleteBook(book.id);
  }

}
