import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { TableModule } from 'primeng/table';
import { log } from 'console';
import { DashboardComponent } from "./components/dashboard/dashboard.component";
@Component({
  selector: 'app-root',
  imports: [ DashboardComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss',
  standalone:true
})
export class App implements OnInit{
  // protected readonly title = signal('GeoLocation_Project');
  // http=inject(HttpClient);
  // employee:any;
  // id=29;
async ngOnInit() {
    // try {
    //   // Use firstValueFrom to convert the Observable to a Promise
    //   const weatherdata = await firstValueFrom(
    //     this.http.get<any[]>('/WeatherForecast')
    //   );

    //   const getemploeyee=await firstValueFrom(
    //     this.http.get<any[]>('/Crud/getemp')

    //   )
    //   const findemp=await firstValueFrom(
    //     this.http.get<any>(`/Crud/findemp/${this.id}`)

    //   )
    //   console.log(findemp);

    //   this.employee=getemploeyee;
    //   console.log(getemploeyee);
    //   this.removeemp();
    //   console.log('Weather Data:', weatherdata);
    // } catch (error) {
    //   console.error('Error fetching data:', error);
    // }
  }

  // async addemployee()
  // {
    
  //      await this.http.post<any[]>('/Crud/postemp',{id:0,emp:'rmer',Department:'Its'}).subscribe(async (e)=>{
  //        if(e)
  //        {
  //         console.log(e);
  //        const getemploeyee=await firstValueFrom(
  //       this.http.get<any[]>('/Crud/getemp'))

      
  //     this.employee=getemploeyee;
  //        }
  //       })
  // }
  
  // async removeemp()
  // {
  //    await  this.http.get<any>(`/Crud/removeemp/${this.id}`).subscribe((res)=>{
  //       if(res)
  //       {
  //         console.log(res)
  //       }
  //       })

      
  // }
}
