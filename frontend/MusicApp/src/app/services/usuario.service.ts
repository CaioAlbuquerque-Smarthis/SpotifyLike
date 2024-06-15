import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from '../model/usuario';
import { Musica } from '../model/album';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private urlToken = "https://localhost:7064/connect/token"
  private url = "https://localhost:7004/api/User"

  constructor(private http: HttpClient) { }

  public autenticar(email:string, senha: string) : Observable<any>{

    let body = new URLSearchParams();
    body.set("username", email);
    body.set("password", senha);
    body.set("client_id", "client-angular-spotify");
    body.set("client_secret", "SpotifyLikeSecret");
    body.set("grant_type", "password");
    body.set("scope", "SpotifyLikeScope");

    let options = {
      headers: new HttpHeaders().set("Content-Type", "application/x-www-form-urlencoded")
    }
    
    return this.http.post(`${this.urlToken}`, body.toString(), options);
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
    }, this.setAuthenticationHeader());
  }

  public getPlaylistFavoritas(idUsuario: string) : Observable<Musica[]>
  {
    return this.http.get<Musica[]>(`${this.url}/favoritas/${idUsuario}`, this.setAuthenticationHeader())
  }

  private setAuthenticationHeader() {

    let access_token = sessionStorage.getItem("access_token");

    let options = {
      headers: new HttpHeaders().set("Authorization", `Bearer ${access_token}`)
    }

    return options;
  }
}
