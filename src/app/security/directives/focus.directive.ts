import { Directive, ElementRef, OnInit, Renderer } from '@angular/core';

@Directive({ selector: '[appFocus]' })

export class FocusDirective implements OnInit {
    constructor(private el: ElementRef, private renderer: Renderer) {
        // focus won't work at construction time - too early
    }

    ngOnInit() {
        this.renderer.invokeElementMethod(this.el.nativeElement, 'focus', []);
    }
}
