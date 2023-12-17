import { BlogPostService } from './../services/blog-post.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPost } from '../models/blog-post.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-edit-blog-post',
  templateUrl: './edit-blog-post.component.html',
  styleUrls: ['./edit-blog-post.component.css']
})
export class EditBlogPostComponent implements OnInit, OnDestroy {

  id: string | null = null;
  model?: BlogPost;
  paramsSubscription?: Subscription;
  // blogPost?: BlogPost;
  updateBlogPostSubscription?: Subscription;

  constructor(private route: ActivatedRoute, private blogPostService: BlogPostService) { }

  handleSubmit(): void { }

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.blogPostService.getBlogPostById(this.id)
            .subscribe({
              next: (response) => {
                this.model = response;
                console.log(this.model);
              }
            });
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
  }

}
