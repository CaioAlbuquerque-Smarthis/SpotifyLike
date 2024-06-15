import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from '../model/usuario';
import { Musica } from '../model/album';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private url = "https://localhost:5086/connect/token"

  constructor(private http: HttpClient) { }

  public autenticar(email:string, senha: string) : Observable<Usuario>{

    let body = new URLSearchParams();
    body.set("username", email);
    body.set("password", senha);
    body.set
    return this.http.post(`${this.url}`)

    return this.http.post<Usuario>(`${this.url}/login`,{
      email:email,
      senha:senha
    });
  }

  public favoritar(idMusica: string, nomeMusica: String, duracaoMusica: string, idUsuario: string) : Observable<Usuario>
  {
    return this.http.post<Usuario>(`${this.url}/Favoritar/${idUsuario}`,{
      id:idMusica,
      nome:nomeMusica,
      duracao: 
      {
        valor:duracaoMusica
      }
    });
  }

  public getPlaylistFavoritas(idUsuario: string) : Observable<Musica[]>
  {
    return this.http.get<Musica[]>(`${this.url}/favoritas/${idUsuario}`)
  }
}
