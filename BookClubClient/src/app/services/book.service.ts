import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Book } from 'src/app/models/book.model';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private url = 'Book';

  constructor(private http: HttpClient) {}

  public getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(`${environment.apiURL}/${this.url}/getBooks`);
  }

  public createBook(book: Book) {
    return this.http.post(`${environment.apiURL}/${this.url}/createBook`, 
      book);
  }

  public updateBook(book: Book) {
    return this.http.put(`${environment.apiURL}/${this.url}/updateBook`, 
      book);
  }

  public deleteBook(book: Book) {
    return this.http.delete(
      `${environment.apiURL}/${this.url}/deleteBook/${book.id}`
    );
  }
}
