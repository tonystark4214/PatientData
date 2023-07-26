import { Component,ViewChild,OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table'; 
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatSnackBar} from '@angular/material/snack-bar';
import { ServiceService } from '../Services/service.service';
import { formatDate } from '@angular/common';
import { Router } from '@angular/router';
@Component({
  selector: 'app-get-data',
  templateUrl: './get-data.component.html',
  styleUrls: ['./get-data.component.css']
})
export class GetDataComponent implements OnInit{
  constructor(private _snackBar: MatSnackBar,private data1:ServiceService,private router:Router){}
  @ViewChild('page') page!:MatPaginator;
  @ViewChild(MatSort) private sort!: MatSort 
  value:any=[];
  message:any;
  username:string="";
  ngOnInit(){
    this.showData();
  }
  showData(){
    this.data1.getAllData().subscribe((res:any)=>{
      this.value=res;
      this.dataSource=new MatTableDataSource(this.value.allPatientData);
      this.dataSource.paginator=this.page;
      this.dataSource.sort=this.sort;
    })
  }
  openSnackBar(message: string,action='') {
    this._snackBar.open(message,action='',{
      duration: 5000,
      verticalPosition: 'top',
      horizontalPosition:'right',
    });
  }
Filter(filterData:any){
  const filterValue = (filterData.target as HTMLInputElement).value;
  this.dataSource.filter = filterValue.trim().toLowerCase();
}
Update(data:any){
  this.router.navigate(['postData'], { queryParams: {id: data}})
}
Delete(data:any){
  this.username="smartdata"+data.name.slice(0,3)+data.name.slice(-2);
  this.data1.DeleteData(data.id,this.username).subscribe((res)=>{
    console.log(res)
    this.message=res;
    if(this.message.message=="User Deleted"){
      this.openSnackBar(this.message.message);
    }
    else{
      this.openSnackBar("no output")
    }
  }) 
  this.showData();
}
  dataSource=new MatTableDataSource<any>(this.value);
  displayedColumns: string[] = ["name","email","userName","phoneNumber","dob","countryName","edit","delete"]
}
