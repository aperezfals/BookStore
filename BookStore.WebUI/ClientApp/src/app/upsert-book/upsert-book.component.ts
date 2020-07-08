import { Component, OnInit, Input } from '@angular/core';
import { Book, Author } from '../books.models';
import { BooksService } from '../books.service';
import { AuthorsService } from '../authors.service';

@Component({
  selector: 'app-upsert-book',
  templateUrl: './upsert-book.component.html'
})
export class UpsertBook implements OnInit {
  @Input()
  book: Book;

  authors: Author[];

  constructor(private booksService: BooksService,
    private authorsService: AuthorsService) {
  }

  ngOnInit(): void {
    this.authorsService.getAll()
      .subscribe(data => {
        this.authors = data.authors;
      });
  }

  saveBook() {
    this.booksService.upsertBook(this.book);
  }

}
