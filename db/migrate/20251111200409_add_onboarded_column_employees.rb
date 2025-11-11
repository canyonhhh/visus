class AddOnboardedColumnEmployees < ActiveRecord::Migration[8.1]
  def change
    add_column :employees, :onboarded, :boolean, default: false, null: false
  end
end
