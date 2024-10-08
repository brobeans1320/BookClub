import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookListComponent } from './components/book/book-list/book-list.component';

const routes: Routes = [
  {
    path: 'mybooks',
    component: BookListComponent,
  },
  // { path: '', redirectTo: '/books', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
