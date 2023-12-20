import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../category/models/category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blog-post',
  templateUrl: './add-blog-post.component.html',
  styleUrls: ['./add-blog-post.component.css']
})
export class AddBlogPostComponent implements OnInit, OnDestroy {
  isImageSelectorVisible: boolean = false;
  imageSelectSubscription?: Subscription;

  model: AddBlogPost;
  categories$?: Observable<Category[]>;

  constructor(
    private blogPostService: BlogPostService,
    private router: Router,
    private categoryService: CategoryService,
    private imageService: ImageService) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      urlHandle: '',
      publishedDate: new Date(),
      author: '',
      isVisible: true,
      categories: []
    }

  }
  ngOnDestroy(): void {
    if (this.imageSelectSubscription) {
      this.imageSelectSubscription.unsubscribe();
    }
  }
  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();

    this.imageSelectSubscription = this.imageService.onSelectImage()
      .subscribe({
        next: (selectedImage) => {
          this.model.featuredImageUrl = selectedImage.url;
          this.isImageSelectorVisible = false;
        }
      });
  }

  toggleImageSelector(): void {
    this.isImageSelectorVisible = !this.isImageSelectorVisible;
  }

  handleSubmit(): void {
    console.log(this.model);
    this.blogPostService.createBlogPost(this.model)
      .subscribe({
        next: () => {
          this.router.navigateByUrl('/admin/blogposts');
        }
      });
  }
}
