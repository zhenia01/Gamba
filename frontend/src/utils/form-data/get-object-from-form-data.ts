const getFormDataObjectFromRequest = async <T extends object>(
  request: Request,
) => Object.fromEntries(await request.formData()) as T;

export { getFormDataObjectFromRequest };
