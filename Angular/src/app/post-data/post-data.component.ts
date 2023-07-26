import { Component,OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { formatDate } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import {MatSnackBar} from '@angular/material/snack-bar';
import { ServiceService } from '../Services/service.service';
@Component({
  selector: 'app-post-data',
  templateUrl: './post-data.component.html',
  styleUrls: ['./post-data.component.css']
})
export class PostDataComponent implements OnInit{
  value: any ;
  preId:number=0;
  username:string="";
  message:any;
  messages:string="no output";
  Country:any;
  constructor(private router:ActivatedRoute,private _snackbar:MatSnackBar,private data1:ServiceService) { }
  PostForm: any = FormGroup
  ngOnInit() {
    let builder = new FormBuilder();
    this.PostForm = builder.group({
      firstName: new FormControl("", Validators.compose([Validators.required,Validators.minLength(2)])),
      lastName: new FormControl("", Validators.compose([Validators.required,Validators.minLength(2)])),
      email: new FormControl("",Validators.compose([Validators.required,Validators.email])),
      dob: new FormControl("", Validators.compose([Validators.required])),
      phoneNumber: new FormControl("",Validators.compose([Validators.required,Validators.pattern('[- +()0-9]+')])),
      country:new FormControl("",Validators.compose([Validators.required]))
    })
    this.router.queryParams.subscribe(param => {
      this.preId=param['id'];
    });
    this.getById(this.preId);
    this.data1.Country().subscribe((res)=>{
      this.Country=res;
    })
  }
  PostData(data: any) { 
    data.id=this.preId;

    console.log(data)
    this.username="smartdata"+data.firstName.slice(0,3)+data.lastName.slice(-2);
    data.userName=this.username;
    data.UpdatedBy=this.username;
    this.data1.PostData(data).subscribe((res) => {
      this.message=res;
      if(this.message.message=="Patient Added" || this.message.message=="Data Updated" || this.message.message=="nothing to update"){
        this.openSnackBar(this.message.message);
      }
      else{
        this.openSnackBar("no output")
      }
  
    })
}
getById(idData: number) {

  this.data1.getbyid(idData).subscribe((res) => {
    
    this.value = res;
    console.log(this.value.patientData)
    this.PostForm.patchValue( this.value.patientData);
    this.PostForm.patchValue({
      country:this.value.patientData.countryID
    })
    this.PostForm.patchValue({dob:formatDate(this.value.patientData.dob,'yyyy-MM-dd','en')});
  })
}

openSnackBar(message: string,action='') {
  this._snackbar.open(message,action='',{
    duration: 5000,
    verticalPosition: 'top',
    horizontalPosition:'right',
  });
}
}
