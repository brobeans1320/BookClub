import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BookService } from 'src/app/services/book.service';
import { BookListComponent } from './components/book/book-list/book-list.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';

@NgModule({
  declarations: [AppComponent, BookListComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
  ],
  providers: [BookService],
  bootstrap: [AppComponent],
})
export class AppModule {}
