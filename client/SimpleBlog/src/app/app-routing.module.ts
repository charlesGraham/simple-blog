import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { BlogPostListComponent } from './features/blog-post/blog-post-list/blog-post-list.component';
import { AddBlogPostComponent } from './features/blog-post/add-blog-post/add-blog-post.component';
import { EditBlogPostComponent } from './features/blog-post/edit-blog-post/edit-blog-post.component';
import { HomeComponent } from './features/public/home/home.component';
import { BlogDetailsComponent } from './features/public/blog-details/blog-details.component';
import { LoginComponent } from './features/auth/login/login.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'admin/categories', component: CategoryListComponent },
  { path: 'admin/categories/add', component: AddCategoryComponent },
  { path: 'admin/categories/:id', component: EditCategoryComponent },
  { path: 'admin/blogposts', component: BlogPostListComponent },
  { path: 'admin/blogposts/add', component: AddBlogPostComponent },
  { path: 'admin/blogposts/:id', component: EditBlogPostComponent },
  { path: 'blog/:url', component: BlogDetailsComponent },
  { path: 'login', component: LoginComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
