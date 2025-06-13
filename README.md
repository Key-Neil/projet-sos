# 📦 projet-sos

## 📝 Description

Ce projet est une application **full stack** composée de :

* Un **frontend Angular** (interface utilisateur)
* Un **backend .NET Core** (API REST)

---

## 📁 Structure du projet

```
/projet-sos
├── frontend/      # Application Angular
└── backend/       # API .NET Core
```

---

## ⚙️ Prérequis

### Frontend (Angular)

* [Node.js](https://nodejs.org/) (v16 ou supérieur recommandé)
* Angular CLI :

  ```bash
  npm install -g @angular/cli
  ```

### Backend (.NET)

* [.NET SDK 6.0+](https://dotnet.microsoft.com/en-us/download)

---

## 🚀 Lancer le projet

### 1. Cloner le dépôt

```bash
git clone https://github.com/ton-utilisateur/ton-repo.git
cd ton-repo
```

---

### 2. Lancer le backend (.NET)

```bash
cd backend
dotnet restore
dotnet build
dotnet run
```

> L’API sera disponible par défaut à l’adresse : `https://localhost:5001` ou `http://localhost:5000`.

---

### 3. Lancer le frontend (Angular)

```bash
cd frontend
npm install
ng serve
```

> L'application Angular sera accessible à : [http://localhost:4200](http://localhost:4200)

---

## 🔧 Configuration

Si nécessaire, configure l’URL de l’API dans le fichier :

```ts
// frontend/src/environments/environment.ts
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001/api'  // Modifier selon votre configuration
};
```

---

## ✅ Compilation pour production

### Frontend

```bash
ng build --configuration production
```

### Backend

```bash
dotnet publish -c Release -o ./publish
```

---

## 📄 Licence

Ce projet est sous licence MIT.
