import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Book } from 'src/app/models/book.model';

@Component({
  selector: 'app-book-view-flyout',
  templateUrl: './book-view-flyout.component.html',
  styleUrls: ['./book-view-flyout.component.css'],
})
export class BookViewFlyoutComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: Book) {}
}
