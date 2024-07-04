import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Book } from 'src/app/models/book.model';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css'],
})
export class BookListComponent implements OnInit {
  title = 'BookList.UI';
  book?: Book;
  books: Book[] = [];
  dataSource: MatTableDataSource<Book> = new MatTableDataSource;
  displayedColumns: string[] = [ 
    "title", 
    "authorFirstName", 
    "authorLastName", 
    "publicationDate", 
    "pageCount", 
    "genre"
  ];

  constructor(private bookService: BookService) {}

  ngOnInit(): void {
    this.getBooks();
    console.log("Hello");
  }

  async getBooks() {
    this.bookService.getBooks().subscribe((data: Book[]) => {
      this.dataSource.data = data
    });
    console.log("I'm working!");
  }
}
