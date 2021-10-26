import {FormControl, NG_VALIDATORS} from "@angular/forms";
import {Directive} from "@angular/core";

@Directive({
    selector: '[dateBirth][ngModel]',
    providers: [
      {
        provide: NG_VALIDATORS,
        useValue: dateBirthValidator,
        multi: true,
      }
    ]
  }
)
export class DateBirthValidator{

}

function dateBirthValidator(control: FormControl) {
    let date = new Date(control.value);

    let now = new Date();

    let maxYear = now.getFullYear() - 16;
    let maxMonth = now.getMonth();
    let maxDay = now.getDate();

    let maxDate = new Date(maxYear, maxMonth, maxDay);

    if (maxDate < date) { // max date will be less than date, because date convert to number (seconds diff from date to 01.01.1970)
      return {
        isCorrectDate: {
          compareResult: true
        }
      }
    }

    return null;
}
