import { Component } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-blog-post',
  templateUrl: './add-blog-post.component.html',
  styleUrls: ['./add-blog-post.component.css']
})
export class AddBlogPostComponent {

  model: AddBlogPost;

  constructor(private blogPostService: BlogPostService, private router: Router) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      urlHandle: '',
      publishedDate: new Date(),
      author: '',
      isVisible: true
    }

  }

  handleSubmit(): void {
    this.blogPostService.createBlogPost(this.model)
      .subscribe({
        next: () => {
          this.router.navigateByUrl('/admin/blogposts');
        }
      });
  }
}
