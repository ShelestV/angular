import { Component } from '@angular/core';
     
@Component({
    selector: 'hello_task_1',
    templateUrl: './app.component.html'
})

export class AppComponent { 
    hideText='Hide text';
    showText='Show text';

    text;
    isHelloVisible=false;

    constructor() {
        this.text = this.showText;
    }

    changeHelloVisibility() {
        this.isHelloVisible = !this.isHelloVisible;
        this.text = this.isHelloVisible ? this.hideText : this.showText;
    }
}