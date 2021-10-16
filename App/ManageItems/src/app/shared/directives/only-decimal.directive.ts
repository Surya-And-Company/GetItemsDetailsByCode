import { Directive, ElementRef, HostListener } from '@angular/core';
import { Config } from 'src/app/models/config.model';

@Directive({
  selector: '[appOnlyDecimal]',
})
export class OnlyDecimalDirective {
  constructor(private el: ElementRef) {}

  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    if (Config.specialKeys.indexOf(event.key) !== -1) {
      return;
    }
    const current: string = this.el.nativeElement.value;
    const next: string = current.concat(event.key);
    if (next && !String(next).match(Config.decimalRegex)) {
      event.preventDefault();
    }
  }
}
