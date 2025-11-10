Rails.application.routes.draw do
  resources :qr_codes
  resources :device_fingerprints
  resources :attendance_records
  resources :students
  resources :activities
  resources :employees
  resources :businesses

  get "up" => "rails/health#show", as: :rails_health_check
end
