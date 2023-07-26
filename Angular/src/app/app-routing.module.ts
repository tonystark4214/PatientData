import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GetDataComponent } from './get-data/get-data.component';
import { PostDataComponent } from './post-data/post-data.component';

const routes: Routes = [
  {
    path:'getAllData',
    component:GetDataComponent
  },
  {
    path:'postData',
    component:PostDataComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
