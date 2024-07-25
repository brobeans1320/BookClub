import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {
  MatPaginator,
  MatPaginatorSelectConfig,
  _MatPaginatorBase,
} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Book } from 'src/app/models/book.model';
import { BookService } from 'src/app/services/book.service';
import { BookViewFlyoutComponent } from '../book-view-flyout/book-view-flyout.component';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css'],
})
export class BookListComponent implements OnInit {
  title = 'BookList.UI';
  book?: Book;
  books: Book[] = [];
  dataSource: MatTableDataSource<Book> = new MatTableDataSource();
  displayedColumns: string[] = [
    'title',
    'authorName',
    'publicationDate',
    'pageCount',
    'genre',
  ];

  constructor(private bookService: BookService, private dialog: MatDialog) {}

  @ViewChild(MatPaginator) matPaginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;

  ngOnInit(): void {
    this.getBooks();
  }

  //Sort books by authorLastName on on init
  async getBooks() {
    this.bookService.getBooks().subscribe((data: Book[]) => {
      this.dataSource.data = data.sort((a, b) =>
        a.authorLastName.localeCompare(b.authorLastName)
      );
      this.dataSource.paginator = this.matPaginator;
      this.dataSource.sort = this.matSort;
    });
  }

  openBookFlyout(row: Book): void {
    this.dialog.open(BookViewFlyoutComponent, { data: row });
  }
}
