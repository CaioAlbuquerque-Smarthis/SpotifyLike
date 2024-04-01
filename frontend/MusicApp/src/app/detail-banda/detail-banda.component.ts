import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Route } from '@angular/router';
import { Banda } from '../model/banda';
import { BandaService } from '../services/banda.service';
import { Album, Musica } from '../model/album';
import { CommonModule } from '@angular/common';
import {MatExpansionModule} from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';


@Component({
  selector: 'app-detail-banda',
  standalone: true,
  imports: [MatExpansionModule, CommonModule, MatIconModule],
  templateUrl: './detail-banda.component.html',
  styleUrl: './detail-banda.component.css'
})
export class DetailBandaComponent implements OnInit {

  idBanda = '';
  banda !: Banda;
  albuns !: Album[];

  constructor(private route: ActivatedRoute, private bandaService: BandaService){}

  ngOnInit(): void {
    this.idBanda = this.route.snapshot.params["id"];

    this.bandaService.getBandaPorId(this.idBanda).subscribe((response: Banda) => {
      this.banda = response;
    });

    this.bandaService.getAlbunsBanda(this.idBanda).subscribe((response: Album[]) => {
      this.albuns = response;
      console.log(this.albuns);
    });

  }

  public favoritar(idMusica:string)
  {

  }


}
