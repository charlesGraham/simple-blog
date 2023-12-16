import { Component } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';

@Component({
  selector: 'app-add-blog-post',
  templateUrl: './add-blog-post.component.html',
  styleUrls: ['./add-blog-post.component.css']
})
export class AddBlogPostComponent {

  model: AddBlogPost;

  constructor() {
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
    console.log(this.model);
  }
}
