import { Component, OnInit } from '@angular/core';
import { Musica } from '../model/album';
import { UsuarioService } from '../services/usuario.service';
import {MatExpansionModule} from '@angular/material/expansion';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-playlist-favoritas',
  standalone: true,
  imports: [MatExpansionModule, CommonModule],
  templateUrl: './playlist-favoritas.component.html',
  styleUrl: './playlist-favoritas.component.css'
})
export class PlaylistFavoritasComponent implements OnInit {

  idUsuario = JSON.parse(sessionStorage.getItem('user') || '{}').id;
  musicas !: Musica[]
  constructor(private usuarioService: UsuarioService){}

  ngOnInit(): void {

    this.usuarioService.getPlaylistFavoritas(this.idUsuario).subscribe((response: Musica[]) => {
      this.musicas = response;
      console.log(this.musicas);
    });
    }
  

}
