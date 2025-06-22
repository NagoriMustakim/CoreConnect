import { Injectable } from '@angular/core';
import data from 'language-list/data.json';

@Injectable({
  providedIn: 'root'
})
export class LanguageListService {

  private languageNameMap: Record<string, string> = {};
  private languageCodeMap: Record<string, string> = {};

  constructor() {
    data.forEach((language: any) => {
      this.languageNameMap[language.language.toLowerCase()] = language.code;
      this.languageCodeMap[language.code.toLowerCase()] = language.language;
    });
  }

  getLanguageCode(name: string): string {
    return this.languageNameMap[name.toLowerCase()];
  }

  getLanguageName(code: string): string {
    return this.languageCodeMap[code.toLowerCase()];
  }

  getLanguageNames(): string[] {
    return data.map((language: any) => language.language);
  }

  getLanguageCodes(): string[] {
    return data.map((language: any) => language.code);
  }

  getData(): any[] {
    return data;
  }

}
