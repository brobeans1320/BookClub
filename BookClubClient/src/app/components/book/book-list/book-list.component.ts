import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorSelectConfig, _MatPaginatorBase } from '@angular/material/paginator';
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
    "authorName", 
    "publicationDate", 
    "pageCount", 
    "genre"
  ];

  constructor(private bookService: BookService) {}

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(): void {
    this.getBooks();
  }

  async getBooks() {
    this.bookService.getBooks().subscribe((data: Book[]) => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
    });
  }
}
