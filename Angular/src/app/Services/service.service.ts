import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  constructor(private http:HttpClient) { }
  
  getAllData(){
    return this.http.get("http://localhost:5211/GetAllPatientData");
  }
  PostData(data:object){
    console.log(data)
    return this.http.post("http://localhost:5211/PostData",data);
  }
  getbyid(id:number){
    return this.http.get("http://localhost:5211/GetById?id="+id)
  }
  DeleteData(id:number,userName:string){
    return this.http.delete("http://localhost:5211/DeleteData?id="+id+"&userName="+userName)
  }
  Country(){
    return this.http.get("http://localhost:5211/Country")
  }
}
