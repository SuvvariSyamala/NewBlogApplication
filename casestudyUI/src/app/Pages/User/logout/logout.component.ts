// logout.component.ts
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
    // Call the logout method when the component is initialized
    this.logout();
  }

  logout(): void {
    // Perform any logout-related tasks here.
    // For example, clearing user information from local storage.
    localStorage.removeItem('token');

    // Navigate to the login page after logout
    this.router.navigate(['/login']);
  }
}
