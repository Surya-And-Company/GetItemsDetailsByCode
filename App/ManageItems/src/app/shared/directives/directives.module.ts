import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OnlyNumberDirective } from './only-number.directive';
import { OnlyDecimalDirective } from './only-decimal.directive';
import { BeginingEndNoSpaceDirective } from './begining-end-no-space.directive';



@NgModule({
  declarations: [OnlyNumberDirective,OnlyDecimalDirective, BeginingEndNoSpaceDirective],
  imports: [
    CommonModule
  ],
  exports: [
    OnlyNumberDirective,
    OnlyDecimalDirective,
    BeginingEndNoSpaceDirective,
  ]
})
export class DirectivesModule { }
