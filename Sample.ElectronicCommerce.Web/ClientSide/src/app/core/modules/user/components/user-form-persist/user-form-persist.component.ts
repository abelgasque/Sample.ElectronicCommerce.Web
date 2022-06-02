import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';

import { ReturnDTO } from 'src/app/shared/util/EntitiesDTO/ReturnDTO';
import { UserEntity } from 'src/app/shared/util/Entities/UserEntity';

import { UserService } from 'src/app/core/modules/user/user.service';

@Component({
  selector: 'app-user-form-persist',
  templateUrl: './user-form-persist.component.html',
  styleUrls: ['./user-form-persist.component.css']
})
export class UserFormPersistComponent implements OnInit {

  @Input() data: UserEntity;

  @Output() eventPersist: EventEmitter<boolean> = new EventEmitter();  

  public form: FormGroup;
  public listRole: any[] = [];
  public listActive: any[] = [
    { name: 'Ativo', value: true },
    { name: 'Inativo', value: false },
  ];  
  public isValidPassword = false;
  public isVisablePassword = false;

  constructor(
    private formBuilder: FormBuilder,    
    private entityService: UserService,
    private sharedService: SharedService,     
    public coreService: CoreService,
  ) { }

  ngOnInit(): void {    
    this.setForm();    
    this.getAllRole();
  }

  private setForm() {
    this.form = this.formBuilder.group({
      id: [{ value: 0, disabled: true }],
      dtCreation: [ {value: null, disabled: true} ],
      dtLastUpdate: [ {value: null, disabled: true} ],
      dtLastBlock: [ {value: null, disabled: true }],
      dtLastDesblock: [ {value: null, disabled: true} ],
      imageUrl: ['./assets/img/Resources/img-user-default.png', [Validators.required]],
      mail: [null, [Validators.required, Validators.email]],
      name: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      password: [null, Validators.required],
      //isValidPassword: [null, Validators.required],
      provider: [ { value: 'Comum', disabled: true }, [Validators.required]],
      codeDesblock: [{value: null, disabled: true }],
      nuCellPhone: [null, [Validators.required]],
      isBlock: [{value: true, disabled: true }, [Validators.required]],
      isActive: [true, [Validators.required]],
      roles: [null],
    }); 
  }
  
  private resetForm() {
    this.form.reset();
    this.form.patchValue(new UserEntity());
  }

  private insert(){    
    this.sharedService.openSpinner();
    this.entityService.Insert(this.data).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          let entity: UserEntity = response.resultObject;          
          entity.dtCreation = new Date(entity.dtCreation);                  
          this.form.patchValue(entity);
        }else{
          this.sharedService.showMessageWarn(response.deMessage);
        }
        this.eventPersist.emit(response.isSuccess);
        this.sharedService.closeSpinner();
      },
      error: (error) => {
        this.coreService.errorHandler(error);        
      }
    });
  }

  private update(){
    this.sharedService.openSpinner();
    this.entityService.Update(this.data).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          let entity: UserEntity = response.resultObject;          
          entity.dtCreation = new Date(entity.dtCreation);
          entity.dtLastUpdate = new Date(entity.dtLastUpdate);
          this.form.patchValue(entity);
        }else{
          this.sharedService.showMessageWarn(response.deMessage);
        }
        this.eventPersist.emit(response.isSuccess);
        this.sharedService.closeSpinner();
      },
      error: (error) => {
        this.coreService.errorHandler(error);
      }
    });
  }

  private persistEntity(){
    if(this.data.id != null){
      this.update();
    }else{ 
      this.insert(); 
    }
  }

  private getAllRole() {
    this.sharedService.openSpinner();
    this.entityService.GetAllRole(true).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          this.listRole = response.resultObject;
        }
        this.sharedService.closeSpinner();
      },
      error: (error) => {
        this.coreService.errorHandler(error);
      }
    });
  }

  public onSubmit(){

    if (this.form.valid) {
      this.persistEntity();
    } else {
      this.sharedService.showMessageWarn("Formulário inválido!");
    }
  }

  public cancelPersist(){
    this.resetForm();
    this.eventPersist.emit(false);
  }

  public onChangedValidPassword(pForm: FormGroup) {            
    if(this.isValidPassword) {      
      pForm.get('password').disable();
    } else {
      pForm.get('password').enable();
    }
  }

  public clickVisablePassword() {
    this.isVisablePassword = (!this.isVisablePassword);
  }
}
