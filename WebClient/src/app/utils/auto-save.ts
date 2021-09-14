import { OnDestroy } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { debounceTime, Observable, Subscription } from 'rxjs';

/**
 * Form data autosave decorator.
 * @param key The key to save the form data to the cache when it changes.
 * @param [storage=localStorage] Cache storage, default is localStorage.
 * @param [object=JSON.parse] Converts a JavaScript Object Notation (JSON) string into an object.
 * @param [value=JSON.stringify] Converts a JavaScript value to a JavaScript Object Notation (JSON) string.
 */
export function autoSave(
  key: string | Promise<string> | Observable<string>,
  storage: Storage = localStorage,
  object: ((text: string,
            reviver?: (this: any, key: string, value: any) => any) => any) = JSON.parse,
  value: ((value: any,
           replacer?: (this: any, key: string, value: any) => any,
           space?: string | number) => string) = JSON.stringify
): PropertyDecorator {
  return (target: Object, propertyKey: string | symbol) => {
    const view = target as OnDestroy;
    let subs: Subscription | undefined;
    const originalOnDestroy = view.ngOnDestroy;

    view.ngOnDestroy = function() {
      subs?.unsubscribe();
      originalOnDestroy?.apply(this);
    };

    let val = (target as { [key: string | symbol]: any; })[propertyKey];

    const getter = () => val;
    const setter = (form: FormGroup) => {
      subs?.unsubscribe();
      val = form;

      if (!form) return;

      if (typeof key === 'string')
        subs = createSetter(storage, key, form, object, value, subs);
      else if (key instanceof Observable)
        key.subscribe({next: k => subs = createSetter(storage, k, form, object, value, subs)});
      else if (key instanceof Promise)
        key.then(k => subs = createSetter(storage, k, form, object, value, subs));
    };

    Object.defineProperty(target, propertyKey, {
      get: getter,
      set: setter
    });
  };
}

/** Create a setter wrapper for a property. */
// tslint:disable-next-line:typedef
function createSetter(
  storage: Storage,
  key: string,
  form: FormGroup,
  object: ((text: string,
            reviver?: (this: any, key: string, value: any) => any) => any),
  value: ((value: any,
           replacer?: (this: any, key: string, value: any) => any,
           space?: string | number) => string),
  subs: Subscription | undefined) {
  subs?.unsubscribe();

  const oldValue = storage.getItem(key);

  if (!!oldValue) {
    form.patchValue(object(oldValue) as { [key: string]: any; });
  }

  return form.valueChanges
    .pipe(debounceTime(100))
    .subscribe({next: ch => storage.setItem(key, value(ch))});
}

/**
 * Clearing the form cache.
 * @param key The key to save the form data to the cache when it changes.
 * @param [storage=localStorage] Cache storage, default is localStorage.
 */
export function autoSaveClear(
  key: string | Promise<string> | Observable<string>,
  storage: Storage = localStorage): void {
  if (typeof key === 'string') {
    storage.removeItem(key);
  }
  else if (key instanceof Observable) {
    key.subscribe({next: k => storage.removeItem(k)});
 }
  else if (key instanceof Promise) {
    key.then(k => storage.removeItem(k));
 }
}
