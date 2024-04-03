import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistFavoritasComponent } from './playlist-favoritas.component';

describe('PlaylistFavoritasComponent', () => {
  let component: PlaylistFavoritasComponent;
  let fixture: ComponentFixture<PlaylistFavoritasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlaylistFavoritasComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PlaylistFavoritasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
