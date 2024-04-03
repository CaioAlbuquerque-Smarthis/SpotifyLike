import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DetailBandaComponent } from './detail-banda/detail-banda.component';
import { LoginComponent } from './login/login.component';
import { BuscaBandaComponent } from './busca-banda/busca-banda.component';
import { PlaylistFavoritasComponent } from './playlist-favoritas/playlist-favoritas.component';

export const routes: Routes = [
    {
        path: '',
        component: LoginComponent
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'detail/:id',
        component: DetailBandaComponent
    },
    {
        path: 'busca/:nome',
        component: BuscaBandaComponent
    },
    {
        path: 'favoritas',
        component: PlaylistFavoritasComponent
    }
];
