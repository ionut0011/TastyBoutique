import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { tap, map } from 'rxjs/operators';
interface CachedImage {
 url: string;
 blob: Blob;
}
@Injectable({
 providedIn: 'root'
})
export class ImageService {
private _cacheUrls: string[] = [];
 private _cachedImages: CachedImage[] = [];
set cacheUrls(urls: string[]) {
 this._cacheUrls = [...urls];
 }
get cacheUrls(): string[] {
 return this._cacheUrls;
 }
set cachedImages(image: CachedImage) {
 this._cachedImages.push(image);
 }

constructor(private http: HttpClient) { }
getImage(url: string) {
const index = this._cachedImages.findIndex(image => image.url === url);
if (index > -1) {
const image = this._cachedImages[index];
return of(URL.createObjectURL(image.blob));
}
return  this.http.get(url, { responseType: 'blob' }).pipe(tap((blob => this.checkAndCacheImage(url, blob)))
);
}
checkAndCacheImage(url: string, blob: Blob) {
  if (this._cacheUrls.indexOf(url) > -1) {
  this._cachedImages.push({url, blob});
  }
  }
}

