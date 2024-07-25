import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BookService } from 'src/app/services/book.service';
import { BookListComponent } from './components/book/book-list/book-list.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSortModule } from '@angular/material/sort';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BookViewFlyoutComponent } from './components/book/book-view-flyout/book-view-flyout.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButton, MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [AppComponent, BookListComponent, BookViewFlyoutComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    BrowserAnimationsModule,
    MatSortModule,
    MatToolbarModule,
    MatDialogModule,
    MatButtonModule,
  ],
  providers: [BookService],
  bootstrap: [AppComponent],
})
export class AppModule {}
