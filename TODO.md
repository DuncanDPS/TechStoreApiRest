# TODOs para TechStoreApiRest

## 🟢 Funcionalidad pendiente

- [ ] Crear un DTO de respuesta y peticion para todas las entidades
- [ ] 
- [ ] Implementar endpoint para obtener usuario por ID
- [ ] Agregar validaciones adicionales en el registro de usuario
- [ ] Mejorar manejo de errores y mensajes en los controladores
- [ ] Documentar todos los endpoints con Swagger
- [ ] Agregar pruebas unitarias para AuthController y UsuarioService

## 🟡 Mejoras y refactorizaciones

- [ ] Revisar y optimizar los mapeos en UsuarioMapper
- [ ] Unificar el manejo de roles y permisos
- [ ] Revisar la configuración de JWT y su seguridad

## 🔴 Bugs conocidos

- [ ] Revisar posibles duplicados de email en condiciones de alta concurrencia

## 📝 Notas

- Recuerda no devolver nunca la contraseña en las respuestas de la API.
- Considerar agregar logs para auditoría de registro y login de usuarios.