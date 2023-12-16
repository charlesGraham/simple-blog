import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogPostListComponent } from './blog-post-list.component';

describe('BlogPostListComponent', () => {
  let component: BlogPostListComponent;
  let fixture: ComponentFixture<BlogPostListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BlogPostListComponent]
    });
    fixture = TestBed.createComponent(BlogPostListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
