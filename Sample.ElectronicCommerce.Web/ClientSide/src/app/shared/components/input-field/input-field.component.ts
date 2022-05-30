import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

import { CoreService } from 'src/app/core/core.service';

const INPUT_FIELD_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => InputFieldComponent),
  multi: true
};

@Component({
  selector: 'app-input-field',
  templateUrl: './input-field.component.html',
  styleUrls: ['./input-field.component.css'],
  providers: [INPUT_FIELD_VALUE_ACCESSOR]
})
export class InputFieldComponent implements ControlValueAccessor  {

  @Input() classeCss;
  @Input() id: string;
  @Input() type: string = 'text';
  @Input() control;
  @Input() label: string;  
  @Input() placeholder: string;
  @Input() isReadOnly: boolean = false;

  private innerValue: any;
  
  get value() {
    return this.innerValue;
  }

  set value(v: any) {
    if (v !== this.innerValue) {
      this.innerValue = v;
      this.onChangeCb(v);
    }
  }

  constructor(public coreService: CoreService) { }  

  onChangeCb: (_: any) => void = () => {};
  onTouchedCb: (_: any) => void = () => {};

  writeValue(v: any): void {
    console.log("writeValue: " + v);
    this.value = v;
  }

  registerOnChange(fn: any): void {
    console.log("registerOnChange " + fn);
    this.onChangeCb = fn;
  }

  registerOnTouched(fn: any): void {
    console.log("registerOnTouched " + fn);
    this.onTouchedCb(fn);
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isReadOnly = isDisabled;
  }
}
