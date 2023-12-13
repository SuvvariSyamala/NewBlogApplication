import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient,HttpClientModule,HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from '../../../Models/user';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-get-all-users',
  standalone: true,
  imports: [HttpClientModule,CommonModule,FormsModule],
  templateUrl: './get-all-users.component.html',
  styleUrl: './get-all-users.component.css'
})
export class GetAllUsersComponent {
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };

  users:User[]=[];
  // // httpOptions = {
  // //   headers: new HttpHeaders({
  // //     'Content-Type': 'application/json',
  // //     Authorization: 'Bearer ' + localStorage.getItem('token'),
  // //   }),
  // };
  user:User;
  userId?:any;
  userName?:any;
  constructor(private http:  HttpClient,private router:Router){
    this.user=new User();
    this.getAllUsers();
    this.userName='';
  }
  getAllUsers(){
    this.http
    .get<User[]>('http://localhost:5293/api/User/GetUsersByRoles',{params:{roles:"User"},headers: this.httpOptions.headers,})
    .subscribe((response)=>{
      this.users=response;
      console.log(this.users);
    })
  }
  searchUser(){
    this.http.get<User>(`http://localhost:5293/api/User/GetUserByName/${this.userName}`,this.httpOptions)
    .subscribe((response)=>{
      this.users=[response];
    })
  }
  block(userID: any) {
    this.userId = userID;
    console.log(typeof this.userId);
    console.log(this.userId);
    this.http
      .post(`http://localhost:5293/api/User/BlockUser/${this.userId}`,{},this.httpOptions)
      .subscribe((response) => {
        console.log(response);
        this.getAllUsers();
      });
  }
  unblock(userId: any) {
    this.userId = userId;
    console.log(typeof this.userId);
    console.log(this.userId);
    this.http
      .post(`http://localhost:5293/api/User/UnBlockUser/${this.userId}`,{},this.httpOptions)
      .subscribe((response) => {
        console.log(response);
        this.getAllUsers();
      });
  }
}

