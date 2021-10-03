import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UnauthorizeComponent } from './unauthorize/unauthorize.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    children: [{ path: '', component: UnauthorizeComponent }],
  },
];

@NgModule({
  declarations: [UnauthorizeComponent],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class UnauthorizeModule {}
