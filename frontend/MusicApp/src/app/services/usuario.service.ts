import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from '../model/usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private url = "https://localhost:7004/api/user"

  constructor(private http: HttpClient) { }

  public autenticar(email:String, senha: String) : Observable<Usuario>{
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
}
