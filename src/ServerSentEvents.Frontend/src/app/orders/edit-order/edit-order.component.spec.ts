import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPhysicianComponent } from './edit-order.component';

describe('EditPhysicianComponent', () => {
  let component: EditPhysicianComponent;
  let fixture: ComponentFixture<EditPhysicianComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditPhysicianComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPhysicianComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
