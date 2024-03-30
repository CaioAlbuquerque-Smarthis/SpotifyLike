import { Component, OnInit } from '@angular/core';
import { BandaService } from '../services/banda.service';
import { Banda } from '../model/banda';
import { Route, ActivatedRoute, Router } from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-busca-banda',
  standalone: true,
  imports: [MatCardModule, CommonModule, FlexLayoutModule, MatButtonModule],
  templateUrl: './busca-banda.component.html',
  styleUrl: './busca-banda.component.css'
})
export class BuscaBandaComponent implements OnInit {

  nomeBanda = '';
  bandas = null;

  constructor(private bandaService: BandaService, private route: ActivatedRoute, private router:Router) {
  }
  ngOnInit(): void {
    this.nomeBanda = this.route.snapshot.params["nome"]
    this.bandaService.getBandaPorNome(this.nomeBanda).subscribe(response => {
      console.log(response);
      this.bandas = response as any;
    });
  }

  public goToDetail(item:Banda){
    this.router.navigate(["detail", item.id]);
  }
}


