import { BlogPostService } from './../services/blog-post.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPost } from '../models/blog-post.model';
import { Observable, Subscription } from 'rxjs';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { UpdateBogPost } from '../models/update-blog-post.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-blog-post',
  templateUrl: './edit-blog-post.component.html',
  styleUrls: ['./edit-blog-post.component.css']
})
export class EditBlogPostComponent implements OnInit, OnDestroy {

  id: string | null = null;
  model?: BlogPost;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];
  paramsSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;
  imageSelectSubscription?: Subscription;
  deleteBlogPostSubscription?: Subscription;
  updateBlogPostSubscription?: Subscription;
  isImageSelectorVisible: boolean = false;

  constructor(private route: ActivatedRoute,
    private blogPostService: BlogPostService,
    private categoryService: CategoryService,
    private router: Router,
    private imageService: ImageService) { }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();

    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.getBlogPostSubscription = this.blogPostService.getBlogPostById(this.id)
            .subscribe({
              next: (response) => {
                this.model = response;
                this.selectedCategories = response.categories.map(category => category.id);
              }
            });
        }

        this.imageSelectSubscription = this.imageService.onSelectImage()
          .subscribe({
            next: (response) => {
              if (this.model) {
                this.model.featuredImageUrl = response.url;
                this.isImageSelectorVisible = false;
              }
            }
          });
      }
    });
  }

  handleSubmit(): void {
    if (this.model && this.id) {
      let updatedBlogPost: UpdateBogPost = {
        author: this.model.author,
        content: this.model.content,
        shortDescription: this.model.shortDescription,
        featuredImageUrl: this.model.featuredImageUrl,
        isVisible: this.model.isVisible,
        publishedDate: this.model.publishedDate,
        title: this.model.title,
        urlHandle: this.model.urlHandle,
        categories: this.selectedCategories ?? []
      };

      this.updateBlogPostSubscription = this.blogPostService.updateBlogPost(this.id, updatedBlogPost)
        .subscribe({
          next: () => {
            this.router.navigateByUrl('/admin/blogposts');
          }
        });
    }
  }

  handleDelete(id: string): void {
    if (this.id) {
      this.deleteBlogPostSubscription = this.blogPostService.deleteBlogPost(id)
        .subscribe({
          next: () => {
            this.router.navigateByUrl('/admin/blogposts');
          }
        })
    }
  }

  toggleImageSelector(): void {
    this.isImageSelectorVisible = !this.isImageSelectorVisible;
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.imageSelectSubscription?.unsubscribe();
    this.deleteBlogPostSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
  }

}
